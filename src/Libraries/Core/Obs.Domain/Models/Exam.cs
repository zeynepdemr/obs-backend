using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obs.Domain.Models;

public class Exam
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    
    public short Point { get; set; }
    public string Grade { get; set; }
    
    [ForeignKey(nameof(LectureId))]
    public virtual Lecture Lecture { get; set; }
    
    [ForeignKey(nameof(StudentId))]
    public virtual Student Student { get; set; }
}