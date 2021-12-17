// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FancyZonesEditor.Models
{
    public class QuickKeysModel : INotifyPropertyChanged
    {
        public SortedDictionary<string, string> SelectedKeys { get; } = new SortedDictionary<string, string>()
        {
            { Properties.Resources.Quick_Key_None, string.Empty },
            { "0", string.Empty },
            { "1", string.Empty },
            { "2", string.Empty },
            { "3", string.Empty },
            { "4", string.Empty },
            { "5", string.Empty },
            { "6", string.Empty },
            { "7", string.Empty },
            { "8", string.Empty },
            { "9", string.Empty },
        };

        public QuickKeysModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void FreeKey(string key)
        {
            if (SelectedKeys.ContainsKey(key))
            {
                SelectedKeys[key] = string.Empty;
                FirePropertyChanged();
            }
        }

        public bool SelectKey(string key, string uuid)
        {
            if (!SelectedKeys.ContainsKey(key))
            {
                return false;
            }

            if (SelectedKeys[key] == uuid)
            {
                return true;
            }

            // clean previous value
            foreach (var pair in SelectedKeys)
            {
                if (pair.Value == uuid)
                {
                    SelectedKeys[pair.Key] = string.Empty;
                    break;
                }
            }

            SelectedKeys[key] = uuid;
            FirePropertyChanged();
            return true;
        }

        public void CleanUp()
        {
            var keys = SelectedKeys.Keys.ToList();
            foreach (var key in keys)
            {
                SelectedKeys[key] = string.Empty;
            }

            FirePropertyChanged();
        }

        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
