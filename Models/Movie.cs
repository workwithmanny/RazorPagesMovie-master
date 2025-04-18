using System.Collections.Generic;
using RazorPagesMovie.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class Movie
{
    public int Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public string? Director { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    
    public string? Genre { get; set; }
    
    // Changed from string to collection of CastMember
    public ICollection<Cast> Cast { get; set; } = new List<Cast>();
    
    [Range(0, 10)]
    [Column(TypeName = "decimal(3,1)")]
    public decimal IMDbRating { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal BoxOfficeRevenue { get; set; }
    
    public string? ReleaseCountry { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}