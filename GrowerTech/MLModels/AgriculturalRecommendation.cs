namespace GrowerTech_MVC.MLModels
{
    public class AgriculturalRecommendation
    {
        public string RecommendedInput { get; set; } = string.Empty;
        public float Confidence { get; set; }

        public AgriculturalRecommendation()
        {
            RecommendedInput = string.Empty;
            Confidence = 0.0f;
        }
    }
}