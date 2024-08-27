using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model_Blog
{
    public class Yazar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Makale> Makaleler { get; set; }

    }
}