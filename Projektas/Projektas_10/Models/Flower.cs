namespace Projektas_10.Models
{
    public class Flower {

        public Flower(
            string flowerId,
            string flowerName,
            string flowerKind,
            double flowerStartingPrice,
            double flowerDiscount,
            string flowerDescription,
            string flowerPhoto)

        {
            FlowerId = flowerId;
            FlowerName = flowerName;
            FlowerKind = flowerKind;
            FlowerStartingPrice = flowerStartingPrice;
            FlowerPhoto = flowerPhoto;
            FlowerDiscount = flowerDiscount;
            FlowerDescription = flowerDescription;
            FlowerEndingPrice = flowerStartingPrice - FlowerDiscount;
        }

        public string FlowerId { get; }
        public string FlowerName { get; }
        public string FlowerKind { get; }
        public string FlowerPhoto { get; }
        public double FlowerStartingPrice { get; }
        public double FlowerDiscount { get; }
        public double FlowerEndingPrice { get; }
        public string FlowerDescription { get; }

        public string GetInformation()
        {
            return $"{FlowerId}_{FlowerName} / {FlowerStartingPrice} - {FlowerDiscount} = {FlowerEndingPrice} / {FlowerDescription}";
        }
    }
}
