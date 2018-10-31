namespace Infrastructure
{
    public interface IRating
    {
        string Id { get; set; }
        string ProductId { get; set; }

        string TimeStamp { get; set; }

        string LocationName { get; set; }

        string Rating { get; set; }

        string UserNotes { get; set; }
    }
}
