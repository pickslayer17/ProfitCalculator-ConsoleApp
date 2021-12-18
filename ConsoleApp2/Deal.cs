namespace ConsoleApp2
{
    public struct Deal
    {
        public int BuyPrice { get; }
        public int SellPrice { get; }
        public int Profit { get; }
        public int DealIndex { get; }
        private int i;
        private int j;
        public Deal(int buyPrice, int sellPrice, int dealIndex, int i, int j)
        {
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            DealIndex = dealIndex;
            this.i = i;
            this.j = j;
            Profit = sellPrice - buyPrice;
        }
        public override string ToString()
        {
            return $" [{DealIndex}] { i} : {j} - [{BuyPrice} , {SellPrice} = {Profit}]";
        }
    }

}
