/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : Create EmrApi.Controllers                                      */
/*                                                                              */
/* Developers : LanHH, Vietnam                                                  */
/* -----------------------------------------------------------------------------*/
/* History 											                            */
/*													                            */
/* Started on : 06 May 2024							                            */
/* Revision : 1.0.0.0 									  	                    */
/* Changed by :     									                        */
/* Change date :                                                                */
/* Changes :                                                                    */
/* Reasons :   										                            */
/********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}