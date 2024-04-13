using System.Collections.Generic;
using System;

public class BulletPool : Pool<Bullet>
{
    public override Bullet Get()
    {
        Bullet bullet = base.Get();

        bullet.TargetReached += Release;

        return bullet;
    }

    public override void Release(Bullet bullet)
    {
        bullet.TargetReached -= Release;

        base.Release(bullet);
    }
}
