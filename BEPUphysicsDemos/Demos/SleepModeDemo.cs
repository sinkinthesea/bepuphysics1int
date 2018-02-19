﻿using System;
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using BEPUutilities;
using FixMath.NET;

namespace BEPUphysicsDemos.Demos
{
    /// <summary>
    /// A large number of blocks fall from the sky into stacks and deactivate.
    /// </summary>
    public class SleepModeDemo : StandardDemo
    {
        private int rowIndex;
        private int updatesSinceLastRow;

        /// <summary>
        /// Constructs a new demo.
        /// </summary>
        /// <param name="game">Game owning this demo.</param>
        public SleepModeDemo(DemosGame game)
            : base(game)
        {
            SpawnRow();

            game.Camera.Position = new Vector3(-30, 30, -30);
            game.Camera.Yaw((Fix64)(-3 * Math.PI / 4));
            game.Camera.Pitch(-(Fix64)Math.PI / 12);
        }

        void SpawnRow()
        {
            //Create a bunch of blocks.
            int zOffset = 5;

            int numColumns = 20;

            Fix64 damping = 0.3m;
            Fix64 verticalOffsetPerColumn = 0.5m;
            Fix64 verticalSpacing = 1.5m;

            Entity toAdd;
            for (int j = 0; j < numColumns; j++)
            {
                for (int k = 1; k <= 7; k++)
                {
                    if (k % 2 == 1)
                    {
                        toAdd = new Box(new Vector3(j * 10 + -3, -0.5m + j * verticalOffsetPerColumn + verticalSpacing * k, rowIndex * 10 + zOffset), 1, 1, 7, 20);
                        toAdd.LinearDamping = damping;
                        toAdd.AngularDamping = damping;
                        Space.Add(toAdd);
                        Game.ModelDrawer.Add(toAdd);
                        toAdd.Tag = "noDisplayObject";
                        toAdd = new Box(new Vector3(j * 10 + 3, -0.5m + j * verticalOffsetPerColumn + verticalSpacing * k, rowIndex * 10 + zOffset), 1, 1, 7, 20);
                        toAdd.LinearDamping = damping;
                        toAdd.AngularDamping = damping;
                        Space.Add(toAdd);
                        Game.ModelDrawer.Add(toAdd);
                        toAdd.Tag = "noDisplayObject";
                    }
                    else
                    {
                        toAdd = new Box(new Vector3(j * 10 + 0, -0.5m + j * verticalOffsetPerColumn + verticalSpacing * k, rowIndex * 10 + zOffset - 3), 7, 1, 1, 20);
                        toAdd.LinearDamping = damping;
                        toAdd.AngularDamping = damping;
                        Space.Add(toAdd);
                        Game.ModelDrawer.Add(toAdd);
                        toAdd.Tag = "noDisplayObject";
                        toAdd = new Box(new Vector3(j * 10 + 0, -0.5m + j * verticalOffsetPerColumn + verticalSpacing * k, rowIndex * 10 + zOffset + 3), 7, 1, 1, 20);
                        toAdd.LinearDamping = damping;
                        toAdd.AngularDamping = damping;
                        Space.Add(toAdd);
                        Game.ModelDrawer.Add(toAdd);
                        toAdd.Tag = "noDisplayObject";
                    }
                }
            }
            var ground = new Box(new Vector3(10 * numColumns * 0.5m - 5, -.5m, rowIndex * 10 + zOffset), 10 * numColumns, 1, 10);
            Space.Add(ground);
            Game.ModelDrawer.Add(ground);
            ground.Tag = "noDisplayObject";
            ++rowIndex;
        }

        public override void Update(Fix64 dt)
        {
            if (rowIndex < 20)
            {
                updatesSinceLastRow++;
                if (updatesSinceLastRow > 120)
                {
                    updatesSinceLastRow = 0;
                    SpawnRow();
                }
            }
            base.Update(dt);
        }

        /// <summary>
        /// Gets the name of the simulation.
        /// </summary>
        public override string Name
        {
            get { return "Sleep Mode Stress Test"; }
        }
    }
}