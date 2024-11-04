namespace GrowerTech_MVC.MLModels 
{
    public class AgriculturalData
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float SoilPH { get; set; }
        public float Rainfall { get; set; }
        public required string CropType { get; set; }
        public required string SoilType { get; set; }    
        public required string ClimateType { get; set; }  
        public required string RecommendedInput { get; set; } 
    }
}