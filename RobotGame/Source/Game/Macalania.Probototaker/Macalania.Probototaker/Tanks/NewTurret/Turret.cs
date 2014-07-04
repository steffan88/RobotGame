﻿using Macalania.YunaEngine.Graphics;
using Macalania.YunaEngine.Rendering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macalania.Probototaker.Tanks.NewTurret
{
    public class Turret
    {
        private TurretComponent[,] _turretComponents;
        private List<TurretModule> _modules = new List<TurretModule>();

        public Turret()
        {
            _turretComponents = new TurretComponent[64, 64];

        }

        public void FireMainGun()
        {
            List<MainGunNew> mgs = GetMainGuns();

            foreach (MainGunNew mg in mgs)
            {
                mg.FireRequest(this);
            }
        }

        private List<MainGunNew> GetMainGuns()
        {
            List<MainGunNew> mainGuns = new List<MainGunNew>();

            foreach (TurretModule p in _modules)
            {
                if (p.GetType().IsSubclassOf(typeof(MainGunNew)))
                {
                    mainGuns.Add((MainGunNew)p);
                }
            }

            return mainGuns;
        }

        public void OnTankDestroy()
        {
            foreach (TurretModule module in _modules)
            {
                module.OnTankDestroy();
            }
        }

        public List<TurretModule> GetModules()
        {
            return _modules;
        }

        public void AddTurretModule(TurretModule module, int x, int y)
        {
            if (CanAddTurretModule(module, x, y) == false)
                throw new Exception("Could not add turret module");

            module.SetLocation(x, y);
            module.AddComponents(this);

            _modules.Add(module);
        }

        public bool CanAddTurretModule(TurretModule module, int x, int y)
        {
            // The module may require that turret bricks are in place at different locations.
            foreach (Point brick in module.RequiredBricks)
            {
                TurretComponent comp = _turretComponents[x + brick.X, y + brick.Y];
                if (comp == null || comp.GetType() != typeof(TurretBrick))
                    return false;
            }

            return true;
        }

        public void AddTurretComponent(TurretComponent component, int x, int y)
        {
            if (_turretComponents[x, y] != null)
                throw new Exception("Can't add turret component");
            _turretComponents[x, y] = component;
            component.SetLocation(x, y);
        }

        public bool CanAddTurretComponent(TurretComponent component, int x, int y)
        {
            return false;
        }

        public void Update(double dt)
        {
            foreach (TurretModule module in _modules)
            {
                module.Update(dt);
            }
        }

        public void Draw(IRender render, Camera camera)
        {
            foreach (TurretComponent tc in _turretComponents)
            {
                if (tc == null)
                    continue;
                tc.Draw(render, camera);
            }

            foreach (TurretModule module in _modules)
            {
                module.Draw(render, camera);
            }
        }
    }
}