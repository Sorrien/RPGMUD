﻿using AetheriaWebService.Controllers;
using AetheriaWebService.DataAccess;
using AetheriaWebService.Hubs;
using AetheriaWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AetheriaWebService.Models.Cell;

namespace AetheriaWebService.Helpers
{
    public class AetheriaHelper
    {
        private AetheriaDataAccess aetheriaDataAccess;
        private ReplicationHelper _replicationHelper;
        public AetheriaHelper(AetheriaDataAccess dataAccess, ReplicationHelper replicationHelper)
        {
            aetheriaDataAccess = dataAccess;
            _replicationHelper = replicationHelper;
        }

        public string ProcessPlayerInput(string input, Player player)
        {
            string response = "";

            var command = CommandHelper.MapCommand(input);

            switch (command)
            {
                case CommandHelper.CommandEnum.Login:
                    response = Login(input, player);
                    break;
                case CommandHelper.CommandEnum.Speak:
                    response = Speak(input, player);
                    break;
                case CommandHelper.CommandEnum.Message:
                    response = Message(input, player);
                    break;
                case CommandHelper.CommandEnum.Take:
                    response = Take(input, player);
                    break;
                case CommandHelper.CommandEnum.Attack:
                    response = Attack(input, player);
                    break;
                case CommandHelper.CommandEnum.Look:
                    response = Look(input, player);
                    break;
                case CommandHelper.CommandEnum.Move:
                    response = Move(input, player);
                    break;
                case CommandHelper.CommandEnum.Lock:
                    response = Lock(input, player);
                    break;
                case CommandHelper.CommandEnum.Unlock:
                    response = Unlock(input, player);
                    break;
                case CommandHelper.CommandEnum.Consume:
                    response = Consume(input, player);
                    break;
                case CommandHelper.CommandEnum.Drop:
                    response = Drop(input, player);
                    break;
                case CommandHelper.CommandEnum.Inventory:
                    response = Inventory(player);
                    break;
                default:
                    response = "I don't understand what you want to do.";
                    break;
            }

            return response;
        }


        public string Login(string input, Player player)
        {
            //take the player's aetheria login data and add their current username to the whitelist of users for that player

            return "not implemented";
        }

        public string Speak(string input, Player player)
        {
            var words = input.Split(" ").ToList();
            words.RemoveAt(0);
            //get all players (and maybe one day npcs) in the current cell and send a message to them telling them that this player spoke and what they said
            var playerStartingPhrase = "You say ";
            var otherStartingPhrase = player.Name + " says ";

            var message = string.Join(" ", words);

            Replicate(otherStartingPhrase + message, player);
            return playerStartingPhrase + message;
        }

        public string Message(string input, Player player)
        {
            //send a message from this player to the target player if they exist

            return "not implemented";
        }

        public string Inventory(Player player)
        {
            var response = "";

            var itemNames = new List<string>();
            foreach (var item in player.Inventory.Entities)
            {
                itemNames.Add(item.Name);
            }
            response = string.Join(", ", itemNames);

            return response;
        }

        public string Take(string input, Player player)
        {
            //determine what the player wants to take by comparing the subject of their sentence with items in the current cell, attempt to add that item to their inventory
            var response = "";
            var words = input.Split(" ").ToList();
            words.RemoveAt(0);
            var itemName = string.Join(" ", words);

            var cell = aetheriaDataAccess.GetCell(player);
            var item = cell.Inventory.Entities.FirstOrDefault(x => x.Name.ToLower().Contains(itemName));

            if (item != null)
            {
                aetheriaDataAccess.UpdateEntityInventory(cell.Inventory, player.Inventory, item);
                var playerStartingPhrase = "You pickup ";
                var otherStartingPhrase = player.Name + " picks up a ";

                response += playerStartingPhrase + item.Name;
                Replicate(otherStartingPhrase + item.Name, player);
            }
            else
            {
                response = "You do not see " + itemName;
            }

            return response;
        }

        public string Drop(string input, Player player)
        {
            var response = "";
            var words = input.Split(" ").ToList();
            words.RemoveAt(0);
            var itemName = string.Join(" ", words);

            var cell = aetheriaDataAccess.GetCell(player);
            var item = player.Inventory.Entities.FirstOrDefault(x => x.Name.ToLower().Contains(itemName));

            if (item != null)
            {
                aetheriaDataAccess.UpdateEntityInventory(player.Inventory, cell.Inventory, item);
                var playerStartingPhrase = "You drop ";
                var otherStartingPhrase = player.Name + " drops a ";

                response += playerStartingPhrase + item.Name;
                Replicate(otherStartingPhrase + item.Name, player);
            }
            else
            {
                response = "You do not have a " + itemName + ".";
            }

            return response;
        }

        public string Attack(string input, Player player)
        {
            //determine the object to attack and attempt to deal damage with the player's currently equipped weapon

            return "not implemented";
        }

        public string Open(string input, Player player)
        {
            //determine what the player wants to open by searching their current cell and attempt to open it, check if its locked, unlock it if the player has the key

            return "not implemented";
        }

        public string Look(string input, Player player)
        {
            //describe the player's current cell
            var description = aetheriaDataAccess.CellDescriptionForPlayer(player);

            Replicate(player.Name + " looks around.", player);

            return description;
        }

        public string Move(string input, Player player)
        {
            var response = "";
            //get direction, move player and return the player's new location
            var word = input.ToLower().Split(" ")[0];
            var direction = DirectionEnum.None;
            switch (word)
            {
                case "north":
                    direction = DirectionEnum.North;
                    break;
                case "south":
                    direction = DirectionEnum.South;
                    break;
                case "west":
                    direction = DirectionEnum.West;
                    break;
                case "east":
                    direction = DirectionEnum.East;
                    break;
                case "up":
                    direction = DirectionEnum.Up;
                    break;
                case "down":
                    direction = DirectionEnum.Down;
                    break;
            }
            var currentCell = aetheriaDataAccess.GetCell(player);
            var newCell = aetheriaDataAccess.GetCellRelativeToCell(currentCell, direction);
            if (newCell != null)
            {
                response += "You move " + direction.ToString() + ".\n";

                Replicate(player.Name + " moves " + direction.ToString(), player);

                aetheriaDataAccess.UpdateEntityCell(player, newCell);

                Replicate(player.Name + " arrives from the " + direction, player);

                response += aetheriaDataAccess.CellDescriptionForPlayer(player);
            }
            else
            {
                response += "You cannot go any further in this direction.";
            }
            return response;
        }

        public string Lock(string input, Player player)
        {
            //attempt to lock an item or door if the player has the key and the target object exists

            return "not implemented";
        }

        public string Unlock(string input, Player player)
        {
            //attempt to unlock an item or door if the player has the key and the target object exists

            return "not implemented";
        }

        private string Consume(string input, Player player)
        {
            //attempt to consume an item or door if the player has the key and the target object exists

            return "not implemented";
        }

        private string GetSubject(string input, Player player)
        {
            //find the subject of the player's sentence

            return input.Split(" ")[1];
        }
        private void Replicate(string message, Player player)
        {
            var chatUsers = aetheriaDataAccess.GetRelevantChatUsersForPlayerAction(player);
            if (chatUsers.Count > 0)
            {
                _replicationHelper.ReplicateToClients(message, chatUsers);
            }
        }
    }
}
