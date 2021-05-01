﻿using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeVector2ViewModel : BinTreePropertyViewModel
    {
        public float X
        {
            get => this._x;
            set
            {
                this._x = value;
                NotifyPropertyChanged();
            }
        }
        public float Y
        {
            get => this._y;
            set
            {
                this._y = value;
                NotifyPropertyChanged();
            }
        }

        private float _x;
        private float _y;

        public BinTreeVector2ViewModel(BinTreeVector2 treeProperty) : base(treeProperty) 
        {
            this.X = treeProperty.Value.X;
            this.Y = treeProperty.Value.Y;
        }
    }
}
