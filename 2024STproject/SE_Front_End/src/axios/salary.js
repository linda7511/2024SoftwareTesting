import { get,del } from './axiosConfig.js'

export async function getSalaryInfo(empId) {
    try {
        const response = await get('/api/Salary/GetByEmployeeId/'+empId, {empId:empId});
        return response.data;
    } catch (error) {
        console.error(error);
        throw error; 
    }
}

export async function delSalary(assetsId) {
    try {
        const response = await del('/api/Salary/Delete/'+assetsId, {});
        return response.data;
    } catch (error) {
        throw error;
    }
}