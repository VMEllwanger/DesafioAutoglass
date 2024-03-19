using System;

public abstract class Entity
{
    public Guid Id { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public override bool Equals(object obj)
    {
        var compararCom = obj as Entity;

        if (ReferenceEquals(this, compararCom)) return true;
        if (ReferenceEquals(null, compararCom)) return false;

        return Id.Equals(compararCom.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

}
