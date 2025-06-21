namespace DevShop.Api.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
            CreateOnUtc = DateTime.UtcNow;
        }
        public string Id { get; private set; }

        public DateTime CreateOnUtc { get; set; }
        public DateTime? UpdateOnUtc { get; set; }
    }
}
