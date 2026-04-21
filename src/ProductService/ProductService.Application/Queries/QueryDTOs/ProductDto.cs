namespace ProductService.Application.Queries.QueryDTOs;

public record ProductDto
{
    public string Id { get; set; } 
    public string Title { get; set; } 
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; } 
    public string ImageUrl { get; set; } 
};