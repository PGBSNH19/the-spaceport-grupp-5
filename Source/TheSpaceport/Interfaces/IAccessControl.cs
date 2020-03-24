namespace TheSpaceport
{
    public interface IAccessControl
    {
        IAccessControl AddNameToCustomer(string name);
        IAccessControl AddFunds();
        IAccessControl StarshipControl();
        IAccessControl Charge();
        IAccessControl AddToDataBase();
    }

    
}