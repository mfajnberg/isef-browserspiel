using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;
using web_api.GameModel.Items;

namespace web_api.GameModel.Creatures
{
    /// <summary>
    /// Creature could be an user avatar or an opponent 
    /// </summary>
    [DebuggerDisplay("Name: {Name}, Party: {PartyId}, Amount of Items: {Backpack.Count}")]
    public class Creature
    {
        /// <summary>
        /// constructor of a creature, instances the backpack
        /// </summary>
        public Creature()
        {
            Backpack = new List<Item>();
        }

        /// <summary>
        /// gets or sets the id of the creature
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the creatures name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the Description of a Creature
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// gets or sets the value of intellect of a creature
        /// </summary>
        public int Intellect { get; set; }

        /// <summary>
        /// gets or sets the value of discipline of a creature
        /// </summary>
        public int Discipline { get; set; }

        /// <summary>
        /// gets or sets the value of power of a creature
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// gets or sets the value of agility of a creature
        /// </summary>
        public int Agility { get; set; }

        /// <summary>
        /// gets or sets the value of lucidity of a creature
        /// </summary>
        public int Lucidity { get; set; }

        /// <summary>
        /// gets or sets the value of charisma of a creature
        /// </summary>
        public int Charisma { get; set; }

        /// <summary>
        /// gets or sets the value of alignment of a creature
        /// </summary>
        public int Alignment { get; set; }

        /// <summary>
        /// gets or sets the value of temperament of a creature
        /// </summary>
        public int Temperament { get; set; }

        /// <summary>
        /// gets or sets the value of morale or a creature
        /// </summary>
        public int Morale { get; set; }

        /// <summary>
        /// gets or sets the party id which the creature belongs to
        /// </summary>
        [ForeignKey("Party")]
        public int? PartyId { get; set; }

        /// <summary>
        /// gets or sets the backpack items
        /// </summary>
        public List<Item>? Backpack { get; set; }
    }
}