﻿using Macalania.Probototaker.Tanks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macalania.Probototaker.Projectiles
{
    class Rocket : Projectile
    {
        public bool Flying { get; set; }
        public float Imprecition { get; set; }
        public Vector2 OriginPosition { get; set; }
        public float FlyDistance { get; set; }

        public Rocket(Tank tankSource, Vector2 position, Vector2 direction, float speed)
            : base(tankSource, position, direction, speed)
        {
        }

        public override void Update(double dt)
        {
            if (Vector2.Distance(OriginPosition, Position) > FlyDistance)
            {
                Explode();
            }
            base.Update(dt);
        }

        public virtual void Explode()
        {
            DestroyGameObject();
        }

        public void Ignite(Vector2 originPosition, float flyDistance)
        {
            OriginPosition = originPosition;
            FlyDistance = flyDistance;
            Flying = true;
        }
    }
}