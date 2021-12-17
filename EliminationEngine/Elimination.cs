﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliminationEngine.GameObjects;
using OpenTK;
using OpenTK.Windowing.Desktop;

namespace EliminationEngine
{
    public class Elimination
    {
        public string Title { get; set; } = "Elimination";
        public EliminationWindow? window = null;
        public Dictionary<Type, EntitySystem> RegisteredSystems = new();
        public float DeltaTime = 0;

        public Elimination(int width, int height)
        {
            var settings = new GameWindowSettings();
            //settings.IsMultiThreaded = true;
            var native = new NativeWindowSettings();
            native.Size = new OpenTK.Mathematics.Vector2i(width, height);
            native.Title = Title;
            native.Flags = OpenTK.Windowing.Common.ContextFlags.ForwardCompatible;
            window = new EliminationWindow(settings, native, this);
        }
        public void Run()
        {
            if (window == null) throw new InvalidDataException("No window was opened");
            window.Run();
        }

        public void AddGameObject(GameObject obj)
        {
            window.GameObjects.Add(obj);
        }

        public void RegisterSystem<EntitySystemType>() where EntitySystemType : EntitySystem
        {
            var system = Activator.CreateInstance<EntitySystemType>();
            system.Engine = this;
            RegisteredSystems.Add(typeof(EntitySystemType), system);
        }
    }
}
