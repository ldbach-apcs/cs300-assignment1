public class Food {

    public Food(
        string _name,
        string _description,
        int _power,
        int _cost,
        byte[] _img,
        int _quantity) {
        name = _name;
        description = _description;
        power = _power;
        cost = _cost;
        img = _img;
        quantity = _quantity;
    }

    public static Food Parse(System.Data.IDataReader reader) {

        string _name = reader.GetString(0);
        string _description = reader.GetString(1);
        int _power = reader.GetInt32(2);
        int _cost = reader.GetInt32(3);
        int _quantity = reader.GetInt32(4);
        byte[] _img = (byte[]) reader["picture"];

        return new Food(_name, _description, _power, _cost, _img, _quantity);
    }

    public string Name() {
        return name;
    }

    public string Description() {
        return description;
    }

    public int Power() {
        return power;
    }

    public int Cost() {
        return cost;
    }

    public byte[] Img() {
        return img;
    }

    public int Quantity() {
        return quantity;
    }

    public int quantity;
    private string name;
    private string description;

    private int power;
    private int cost;   
    private byte[] img;
}