namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public string Type { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }

        public Store() { }
    }
}