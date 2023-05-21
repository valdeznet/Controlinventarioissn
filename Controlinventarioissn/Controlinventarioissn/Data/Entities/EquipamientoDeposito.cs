namespace Controlinventarioissn.Data.Entities
{
    public class EquipamientoDeposito
    {
        public int Id { get; set; }

        public Equipamiento Equipamiento { get; set; }

        public Deposito Deposito { get; set; }

    }
}
