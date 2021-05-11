using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.TransmittedTypes;

namespace CluelessBackend.Core
{
    public class GameManager : Board
    {
        // Instantiate random number generator.  
        private Random random = new Random();
        List<Weapon> weapons_ = new List<Weapon>(6);
        List<Suspect> suspects_ = new List<Suspect>(6);
        Board board_;

        // Create deck of cards
        CardDeck deck_ = new CardDeck();

        // Init scenario file
        ScenarioFile scenarioFile_ = new ScenarioFile();

        /// <summary>
        /// Creating a board object
        /// </summary>
        public void InitBoard()
        {
            board_ = new Board();
        }

        /// <summary>
        /// Init weapons instatiate 6 weapons and adds them the weapons_ lists
        /// </summary>
        public void InitWeapons()
        {
            foreach (Weapon.WEAPON weaponIndex in Enum.GetValues(typeof(Weapon.WEAPON)))
            {
                weapons_.Add(new Weapon(weaponIndex));
            }
        }

        /// <summary>
        /// Init weapons instatiate 6 weapons and adds them the weapons_ lists
        /// </summary>
        public void InitSuspects()
        {
            foreach (SUSPECT suspectIndex in Enum.GetValues(typeof(SUSPECT)))
            {
                suspects_.Add(new Suspect(suspectIndex));
            }
        }

        public void AssignWeaponToRooms()
        {
            // Create a list of 6 random  numbers for each weapon
            List<int> weaponRandomList = CreateUniqueListOfRandomNum(0, 6);
            // Create a list of 3 random  numbers for row and columns of the rooms in the board
            List<int> boardRowRandomList = CreateUniqueListOfRandomNum(0, 3);
            List<int> boardColRandomList = CreateUniqueListOfRandomNum(0, 3);

            int weaponIndex = 0;
            for(int listIndex = 0; listIndex < boardRowRandomList.Count; ++listIndex)
            {
                // Multiply by 2 because rooms are in row location 0,2,4 and random lists has 0,1 and 2
                int roomRowIndex = boardRowRandomList[listIndex] * 2;
                int roomColIndex = boardColRandomList[listIndex] * 2;

                // Place a random weapon in a random room
                rooms_[roomRowIndex, roomColIndex].SetWeaponinRoom(weapons_[weaponRandomList[weaponIndex]]);
                weaponIndex += 1;
            }
        }

        public void StartGame(
            List<IBackendPlayerNetworkModel> networkPlayerModels,
            Dictionary<IBackendPlayerNetworkModel, SUSPECT> suspectSelections)
        {
            // Init players
            List<Player> players = new List<Player>();

            foreach (var playerModel in networkPlayerModels)
                players.Add(new Player(suspectSelections[playerModel]));

            // Init board with rooms and hallways
            InitBoard();
            InitWeapons();
            InitSuspects();

            Console.WriteLine("Creating deck of cards...");
            Console.WriteLine(" - 6 weapons - 6 suspects - 9 rooms - ");
            deck_.CreateDeckOfCards();
            deck_.PrintDeckOfCards();

            // Place 3 random cards in the secret envelope
            scenarioFile_.SetEnvelopeCards(deck_.SelectCardsForEnvelope());
            scenarioFile_.PrintEnvelopeCards();

            Console.WriteLine("\nUpdated deck after selecting 3 cards for the envelope..");
            deck_.PrintDeckOfCards();


            Console.WriteLine("\nShuffling the cards before handing over to the players..");
            deck_.ShuffleCards();
            deck_.PrintDeckOfCards();
            
            // Set players to the board
            GetBoard().SetPlayers(players);
            SpreadCardsToPlayer(players);
            CreateUniqueListOfRandomNum(0, 3);
            AssignWeaponToRooms();
            GetBoard().SePlayerstStartingPosition(players);


            // Send to all players:
            // - The mapping of weapons to rooms
            // Send to each individual player:
            // - Their cards
            // Send to the player who goes first:
            // - Miss Scarlet
            var roomWeaponMap = new Dictionary<Room, Weapon>();
            foreach (var room in rooms_)
            {
                if (room.Gethallway() || room.GetWeaponInRoom() == null)
                    continue;
                roomWeaponMap[room] = room.GetWeaponInRoom();
            }
            foreach (var client in networkPlayerModels)
            {
                var player = players.Single(gamePlayer => gamePlayer.GetSuspectType() == suspectSelections[client]);
                var gameStartInfo = new GameStartInfo
                {
                    RoomWeaponMap = TransformRoomWeaponMapForJson(roomWeaponMap),
                    Cards = player.GetPlayersCards().Select(GetNumberFromCard).ToList(),
                    GoesFirst = player.GetSuspectType() == SUSPECT.MISS_SCARLET
                };
                client.SendGameStartInfo(gameStartInfo);
            }

        }

        private static int GetNumberFromCard(Card card)
        {
            switch (card.Card_Type)
            {
                case Card.CARD_TYPE.SUSPECT:
                    return (int)card.Suspect_Cards;
                case Card.CARD_TYPE.WEAPON:
                    return (int)card.Weapon_Cards;
                case Card.CARD_TYPE.ROOM:
                    return (int)card.Room_Cards;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private static List<(int, int)> TransformRoomWeaponMapForJson(Dictionary<Room, Weapon> toTransform)
        {
            var rtn = new List<(int, int)>();
            foreach (var pair in toTransform)
            {
                rtn.Add(((int)pair.Key.RoomEnum, (int)pair.Value.weaponType));
            }

            return rtn;
        }

        /// <summary>
        /// Function to move the player into a room
        /// </summary>
        /// <param name="player">Player that moves to a another room </param>
        /// <param name="room"> Room that the player should move </param>
        public void MovePlayerToRoom(Player player, Room room)
        {
            room.SetPlayerInRoom(player);
        }

        /// <summary>
        /// Hands the cards to the players in the game
        /// </summary>
        /// <param name="players"> All players in the game </param>
        /// <param name="deck"> Deck of cards that will be handed out to the players </param>
        public void SpreadCardsToPlayer(List<Player> players)
        {
            int cardsCount = 0;

            while (cardsCount < deck_.GetDeckSize())
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    // if cardsCount = deckSize_ all cards have been spread out to the players
                    if (cardsCount == deck_.GetDeckSize())
                    {
                        break;
                    }

                    players[i].HandOneCard(deck_.GetCardFromDeck(cardsCount));
                    cardsCount++;
                }
            }
        }

        /// <summary>
        /// A player makes a suggestion
        /// </summary>
        /// <param name="player"> Player that makes the suggestion </param>
        /// <param name="proposedCards"> Proposed cards for the suggestion </param>
        public void MakeSuggestion(Player player, List<Card> proposedCards)
        {
            Console.WriteLine("Player {playerName_} has made a suggestion");

            player.HasSuggested(); 
              
        }

        /// <summary>
        /// Player makes an accusation
        /// </summary>
        /// <param name="player"> player that makes the accusation </param>
        /// <param name="proposedCards">Proposed card for the accusation </param>
        public bool MakeAccusation(Player player, List<Card> proposedCards)
        {
            Console.WriteLine("Player {playerName_} has made an accusation");
            player.HasAccused();
            player.SetIsActive(false);

            if ((proposedCards[0] == scenarioFile_.GetEnvelopeCards()[0])
                 && (proposedCards[1] == scenarioFile_.GetEnvelopeCards()[1])
                 && (proposedCards[2] == scenarioFile_.GetEnvelopeCards()[2])
                 )
            {
                Console.WriteLine("Win");
                return true;
            }
            else
            {
                return false;
           
            }
        }

        /// <summary>
        /// Move Player to room will move the player to the coordinates of the roomType
        /// Player'c current position will also be updated to the coordinates of the room 
        /// </summary>
        /// <param name="player"> Player that will be moved</param>
        /// <param name="roomType"> Room type that the player requested to move at</param>
        public void MovePlayerToRoom(Player player, Room.ROOM roomType)
        {
            if (roomType == Room.ROOM.STUDY)
            {
                rooms_[0, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 0);
            }
            else if (roomType == Room.ROOM.HALL)
            {
                rooms_[0, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 2);
            }
            else if (roomType == Room.ROOM.LOUNGE)
            {
                rooms_[0, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 4);
            }
            else if (roomType == Room.ROOM.LIBRARY)
            {
                rooms_[2, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 0);
            }
            else if (roomType == Room.ROOM.BILLIARD_ROOM)
            {
                rooms_[2, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 2);
            }
            else if (roomType == Room.ROOM.DINNING_ROOM)
            {
                rooms_[2, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 4);
            }
            else if (roomType == Room.ROOM.CONSERVATORY)
            {
                rooms_[4, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 0);
            }
            else if (roomType == Room.ROOM.BALLROOM)
            {
                rooms_[4, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 2);
            }
            else if (roomType == Room.ROOM.KITCHEN)
            {
                rooms_[4, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 4);
            }
        }

        /// <summary>
        /// Move player to a room or hallway given the next cell coordinates
        /// </summary>
        /// <param name="player"> player that will be moved </param>
        /// <param name="row"> The row that the player will be positioned on the board </param>
        /// <param name="column"> The column that the player will be positioned on the board </param>
        public void MovePlayerToRoom(Player player, int row, int column)
        {
            rooms_[row, column].SetPlayerInRoom(player);
            player.SetPlayerPosition(row, column);
        }

        /// <summary>
        /// Creates a unique lists of random number from min to max (with a size of max)
        /// Eg: CreateUniqueListOfRandomNum(0, 3), will generate a list of 3 numbers, numbers in the list will be 0,1,2, 
        /// in a random order
        /// </summary>
        /// <param name="min"> Possible min number generated </param>
        /// <param name="max"> Possible max number generated </param>
        /// <returns></returns>
        public List<int> CreateUniqueListOfRandomNum(int min, int max)
        {
            List<int> randomList = new List<int>(max);
            int myNumber = 0;

            while (true)
            {
                myNumber = random.Next(min, max);
                if (!randomList.Contains(myNumber))
                {
                    randomList.Add(myNumber);
                }
                if (randomList.Count >= max)
                {
                    break;
                }
            }

            // TODO: remove, just for debugging reasons its here
            for (int i = min; i < max; ++i)
            {
                Console.WriteLine("Number generator for card " + randomList[i]);
            }

            return randomList;
        }

        public void GetPlayerMovementOptions(Player player)
        {

            List<int> playerMovementOptions = new List<int>();

            // Move 1 row down
            int rowMovement = player.GetPlayerPositionRow() - 1;
            int ColMovement = player.GetPlayerPositionCol();

            if(rowMovement < 5 || ColMovement < 5)
            {
                playerMovementOptions.Add(GetRoomTypeBasedOnCoordinates(rowMovement, ColMovement));
            }

            // Move 1 row up
            rowMovement = player.GetPlayerPositionRow() + 1;
            ColMovement = player.GetPlayerPositionCol();

            if (rowMovement < 5 || ColMovement < 5)
            {
                playerMovementOptions.Add(GetRoomTypeBasedOnCoordinates(rowMovement, ColMovement));
            }

            // Move to the left
            rowMovement = player.GetPlayerPositionRow();
            ColMovement = player.GetPlayerPositionCol() - 1;

            if (rowMovement < 5 || ColMovement < 5)
            {
                playerMovementOptions.Add(GetRoomTypeBasedOnCoordinates(rowMovement, ColMovement));
            }

            // Move to the right
            rowMovement = player.GetPlayerPositionRow();
            ColMovement = player.GetPlayerPositionCol() + 1;

            if (rowMovement < 5 || ColMovement < 5)
            {
                playerMovementOptions.Add(GetRoomTypeBasedOnCoordinates(rowMovement, ColMovement));
            }
        }

        int GetRoomTypeBasedOnCoordinates(int row, int column)
        {
            int roomEnumValue = -1;
            if (row == 0 && column == 0)
            {
                roomEnumValue = (int)Room.ROOM.STUDY;
            }
            else if(row == 0 && column == 2)
            {
                roomEnumValue = (int)Room.ROOM.HALL;
            }
            else if(row == 0 && column == 4)
            {
                roomEnumValue = (int)Room.ROOM.LOUNGE;
            }
            else if (row == 2 && column == 0)
            {
                roomEnumValue = (int)Room.ROOM.LIBRARY;
            }
            else if (row == 2 && column == 2)
            {
                roomEnumValue = (int)Room.ROOM.BILLIARD_ROOM;
            }
            else if (row == 2 && column == 4)
            {
                roomEnumValue = (int)Room.ROOM.DINNING_ROOM;
            }
            else if (row == 4 && column == 0)
            {
                roomEnumValue = (int)Room.ROOM.CONSERVATORY;
            }
            else if (row == 4 && column == 2)
            {
                roomEnumValue = (int)Room.ROOM.BALLROOM;
            }
            else if (row == 4 && column == 4)
            {
                roomEnumValue = (int)Room.ROOM.KITCHEN;
            }

            return roomEnumValue;
            
        }
        public Board GetBoard()
        {
            return board_;
        }

        public void SetCardDeck(CardDeck deck)
        {
            deck_ = deck;
        }
    }
}
