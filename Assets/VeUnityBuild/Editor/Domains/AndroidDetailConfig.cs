﻿using System;
using UnityEditor;

namespace VeUnityBuild.Editor.Domains
{
    [Serializable]
    public class AndroidDetailConfig
    {
        public BuildOptions buildOptions;

        public AndroidDetailConfig(BuildOptions buildOptions)
        {
            this.buildOptions = buildOptions;
        }
    }
}
