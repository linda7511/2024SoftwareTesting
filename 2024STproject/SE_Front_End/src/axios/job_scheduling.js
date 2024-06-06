import { get } from './axiosConfig.js'
export async function fetchEmployeeData() {
    try {
        const response = await get(`/api/Employee/GetAll`, {});
        console.log(response);
        return response.data;
    } catch (error) {
        console.error(error);
        throw error; 
    }
}

export async function fetchDepartmentData() {
    try {
        const postResponse1 = await get('/api/Post/GetAll', {});
        const postResponse=postResponse1.data;
        /*对每条返回的岗位i数据，根据DepartmentId获取DepartmentName */
        const departmentData = await Promise.all(postResponse.map(async post => {
            const departmentResponse = await get('/api/Department/Get/' + post.departmentId, {});
            return {
                ...post,
                departmentName: departmentResponse.data.departmentName,
                numberOfPeople: departmentResponse.data.numberOfPeople
            };
        }));
        console.log(departmentData);
        return departmentData;
    } catch (error) {
        console.error(error);
        throw error; 
    }
}