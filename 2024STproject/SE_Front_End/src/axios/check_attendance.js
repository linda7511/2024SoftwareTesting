import { get } from './axiosConfig.js'
//import axios from "axios"
export async function getAttendanceInfo(empId) {
    try {
        console.log(777)
        console.log(empId)
        const response = await get(`/api/AttendanceInformation/GetByEmpId/${empId}` , {empId:empId});
        console.log(`/api/AttendanceInformation/GetByEmpId/${empId}`);
        console.log(response);
        //console.log(typeof response);
        return response.data;
    } catch (error) {
        console.error(error);
        throw error; 
    }
}
