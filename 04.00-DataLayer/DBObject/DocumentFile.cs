using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject;

public class DocumentFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string HttpLink { get; set; }
    public string? CreatedBy { get; set; }
    public int GroupId { get; set; }
    public Boolean Approved { get; set; }
    public DateTime CreatedDate { get; set; }
}