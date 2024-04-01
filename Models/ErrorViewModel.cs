using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;

namespace OnlineShop.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
//{
//        < table cellpadding = "0" cellspacing = "0" >
//            < tr >
//                < th colspan = "2" align = "center" > Person Details </ th >
//            </ tr >
//            < tr >
//                < td > FirstName: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.FirstName, new
//                    {
//                        required = "required",
//                        minlength = "3",
//                        maxlength = "10"
//                    })
//                </ td >
//            </ tr >
//            < tr >
//                < td > LastName: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.LastName, new
//                    {
//                        required = "required",
//                        minlength = "3",
//                        maxlength = "10"
//                    })
//                </ td >
//            </ tr >
//            < tr >
//                < td > CandidateId: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.CandidateId)
//                </ td >
//            </ tr >
//            < tr >
//                < td > RoleId: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.RoleId)
//                </ td >
//            </ tr >
//            < tr >
//                < td > Email: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.Email)
//                </ td >
//            </ tr >
//            < tr >
//                < td > Password: </ td >
//                < td >
//                    @Html.TextBoxFor(m => m.Password)
//                </ td >
//            </ tr >
//            < tr >
//                < td ></ td >
//                < td >< input type = "submit" value = "Submit" /></ td >
//            </ tr >
//        </ table >
//    }