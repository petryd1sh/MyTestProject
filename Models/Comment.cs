namespace MyTestProject.Models;

public class Comment
{
    public int PostId { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Body { get; set; }
    public override string ToString()
    {
        return $"{typeof(Comment)}: {PostId} {Id} {Email} "; //{Name}{Body}";
    }
}