using System.ComponentModel.DataAnnotations.Schema;
using web_api.GameModel.Items;

namespace web_api.GameModel.Creatures
{
    public class Creature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Intellect { get; set; }
        public int Discipline { get; set; }
        public int Power { get; set; }
        public int Agility { get; set; }
        public int Lucidity { get; set; }
        public int Charisma { get; set; }

        public int Alignment { get; set; }
        public int Temperament { get; set; }
        public int Morale { get; set; }

        
        public Party Party { get; set; }
        public int PartyId { get; set; }
        public List<Item>? Backpack { get; set; }

        public Creature() 
        {
           
        }

    }
}