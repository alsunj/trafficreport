using Base.Contracts.DAL;

namespace App.DAL.EF;

public class DalDummyMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class where TRightObject : class
{
    /*
    Contest? IDalMapper<Contest, Contest>.Map(Contest? inObject)
    {
        return inObject;
    }*/

    public TLeftObject? MapRL(TRightObject? inObject)
    {
        return inObject as TLeftObject;
    }

    public TRightObject? MapLR(TLeftObject? inObject)
    {
        return inObject as TRightObject;
    }
}