using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTS_User_Service.Models
{
    public static class ResponceModel
    {
        public static ResultModel GetResponse(Object data, string msg = "")
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = 0;
                    result.Status = "Success";
                    result.StatusCode = 200;
                    result.Msg = msg == "" ? "Record has been fetched successfully..!!" : msg;
                    result.Data = data;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static ResultModel GetSavedResponse( bool blnResultFlag, int savedId, Object data=null)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? savedId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 500;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been saved..!!" : "Record can not be saved..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel GetUpdatedResponse(bool blnResultFlag, int updatedId, Object data = null)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? updatedId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 500;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been updated..!!" : "Record can not be updated..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel GetExceptionResponse(Exception ex, string strMethodName)
        {

            using (ResultModel result = new ResultModel())
            {
                result.Id = -1;
                result.Status = "Error";
                result.StatusCode = 500;
                result.Msg = ex.Message;
                result.ErrorMsg = ex.Message;
                return result;
            }
        }
    }
}
