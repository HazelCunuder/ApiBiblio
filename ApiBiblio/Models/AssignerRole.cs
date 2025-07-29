namespace ApiBiblio.Models
{
    public class AssignerRole
    {
        // Relation avec le Role
        public int RoleId { get; set; }
        public int EmployeId { get; set; }

        public Role? Role { get; set; }
        public Employe? Employe { get; set; }
    }
}
