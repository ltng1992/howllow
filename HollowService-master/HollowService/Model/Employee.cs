using System.ComponentModel.DataAnnotations;

namespace HollowService.Model
{
    public class Employee
    {
        private int _id;
        [Key]
        public int Id { get { return _id; } set { _id = value; } }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }

    }
}
