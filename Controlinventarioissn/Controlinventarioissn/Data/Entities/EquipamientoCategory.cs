namespace Controlinventarioissn.Data.Entities
{
    public class EquipamientoCategory
    {
        public int Id { get; set; }

        public Equipamiento Equipamiento { get; set; }

        public Category Category { get; set; }

    }
}
