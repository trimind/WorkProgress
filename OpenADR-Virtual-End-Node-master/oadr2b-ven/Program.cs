//////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2014, Electric Power Research Institute (EPRI)
// All rights reserved.
//
// oadr2b-ven, oadrlib, and oadr-test ("this software") are licensed under the 
// BSD 3-Clause license.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//   * Redistributions of source code must retain the above copyright notice, this 
//     list of conditions and the following disclaimer.
//     
//   * Redistributions in binary form must reproduce the above copyright notice, 
//     this list of conditions and the following disclaimer in the documentation 
//     and/or other materials provided with the distribution.
//     
//   * Neither the name of EPRI nor the names of its contributors may 
//     be used to endorse or promote products derived from this software without 
//     specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace oadr2b_ven
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //#if (!DEBUG)
            //            frmSplash splash = new frmSplash();

            //            DialogResult result = splash.ShowDialog();

            //            if (result != System.Windows.Forms.DialogResult.OK)
            //                return;
            //#endif
            int maxThread = 15;
             int NumberOfLoad = 10;
             int VenNum = -1;

            string VenAccount = null;// "num:1=Name:wssds,id:sdsd|name:sdsd,id:reeere";

            string[] args = Environment.GetCommandLineArgs();
           // MessageBox.Show(args.Length.ToString());
            if (args.Length == 5)
            {
                try
                {
                    VenAccount = args[4];
                  
                    NumberOfLoad = int.Parse(args[2]);
                  
                    VenNum = int.Parse(args[1]);
                  
                    maxThread = int.Parse(args[3]);
                  
                }
                catch
                {
                    Application.Exit();

                }
            }
             if (VenNum == -1 || VenAccount==null) return;

           // MessageBox.Show(maxThread.ToString());

                var arr = (VenAccount ?? "").Split('=');
                var accs = arr[1].Split(new char[] { '|' }).ToList();
           
            // int maxThread = 5;
            var tasks = new Thread[maxThread];

                     if (maxThread > accs.Count *2)
                        {
                             MessageBox.Show("maxThread không lớn hơn maxacc x 2");
                            return;
                        }
                        else if (accs.Count < maxThread)
                        {
                            var tt = accs.OrderBy(x => Guid.NewGuid()).Take(maxThread - accs.Count).ToArray();
                            accs.AddRange(tt);
                        }

            var range = Enumerable.Range((VenNum - 1) * maxThread + 1, maxThread).OrderByDescending(a => a);
            int index = -1;
          
            foreach (var i in range)
            {
                int workerIndex = i;
                index++;
                int iindex = index;
                  tasks[index] = new Thread(() =>
                {
                    var f = new frmMain();
                    f.Text = "Ven " + workerIndex.ToString();
                    f.VenNum = workerIndex;
                    f.NumberOfLoad = NumberOfLoad;
                    f.VenAccount = accs[iindex];
                    Application.Run(f);
                });
                tasks[index].SetApartmentState(ApartmentState.STA);
                tasks[index].Start();

            }
         


          
        }
    }
}
