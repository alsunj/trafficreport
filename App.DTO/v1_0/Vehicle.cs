namespace App.DTO.v1_0
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        
        public Guid VehicleTypeId { get; set; }
        
        public string? Color { get; set; }
        
        public string? RegNr { get; set; }
        
        public decimal? Rating { get; set; }
    }
    public class VehicleModifyDTO
    {
        public Guid Id { get; set; }
        
        public Guid VehicleTypeId { get; set; }
        
        public string? Color { get; set; }
        
        public string? RegNr { get; set; }
        
        // No Rating field here
    }
    
}