using System;

public abstract class Entidade
{
	public Guid Id { get; set; }

	public Entidade()
	{
		Id = Guid.NewGuid();
	}


	public override bool Equals(object obj)
	{
		var compararCom = obj as Entidade;

		if (ReferenceEqual(this, compararCom)) return true;
        if (ReferenceEqual(null, compararCom)) return false;
    }
}
