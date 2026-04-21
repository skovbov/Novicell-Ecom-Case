namespace ProductService.Application.Queries.QueryDTOs;

public record CategoryDto
{
    public string Id { get; set; } 
    public string Name { get; set; } 
    public string Description { get; set; } 
};