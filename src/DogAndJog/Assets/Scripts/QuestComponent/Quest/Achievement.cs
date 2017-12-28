// using Mono.Data.Sqlite;
public class Achievement {
    string name {get; set;}
    string description {get; set;}
    int shareRequirement {get; set;}
    double distanceRequirement {get; set;}
    int petLevelRequirement { get; set;}
    int rewardExp {get ; set;}
    int rewardMoney {get ; set;}

    public Achievement(
        string name,
        string description,
        int shareRequirement,
        double distanceRequirement,
        int petLevelRequirement,
        int rewardExp,
        int rewardMoney) {
        this.name = name;
        this.description = description;
        this.shareRequirement = shareRequirement;
        this.distanceRequirement = distanceRequirement;
        this.petLevelRequirement = petLevelRequirement;
        this.rewardExp = rewardExp;
        this.rewardMoney = rewardMoney;
    }


    public static Achievement Parse(System.Data.IDataReader reader) {
        string _name = reader.GetString(0);
        string _description = reader.GetString(1);
        int _shareRequirement = reader.GetInt32(2);
        double _distanceRequirement = reader.GetDouble(3);
        int _petLevelRequirement = reader.GetInt32(4);
        int _rewardExp = reader.GetInt32(5);
        int _rewardMoney = reader.GetInt32(6);

        return new Achievement(
            _name,
            _description,
            _shareRequirement,
            _distanceRequirement,
            _petLevelRequirement,
            _rewardExp,
            _rewardMoney
        );
    }
}