import axios from 'axios'
//需要拦截器的地方使用instance对象， 有自定义返回逻辑的地方沿用axios，在组件内部处理返回结果即可
import instance from './filter'
const http = "/api"; 

//获取token
export const getToken = (name: string, password: string) => {
    return instance.get(http + "/Login/GetToken?name=" + name + "&password=" + password);
}


//邮件信息模块
//获取列表
export const getEmaillogDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Emaillog/GetEmaillogs", parms)
}
//添加
export const addEmaillog = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Emaillog/Add", parms)
}
//修改
export const editEmaillog = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Emaillog/Edit", parms)
}
//删除
export const delEmaillog = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Emaillog/Del?id=" + id)
}
//BatchDel
export const batchDelEmaillog = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Emaillog/BatchDel?ids=" + ids)
}



//客户信息模块
//获取列表
export const getClientDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Client/GetClients", parms)
}
//添加
export const addClient = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Client/Add", parms)
}
//修改
export const editClient = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Client/Edit", parms)
}
//删除
export const delClient = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Client/Del?id=" + id)
}
//BatchDel
export const batchDelClient = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Client/BatchDel?ids=" + ids)
}



//合同信息模块
//获取列表
export const getContractDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Contract/GetContracts", parms)
}
//添加
export const addContract = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Contract/Add", parms)
}
//修改
export const editContract = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Contract/Edit", parms)
}
//删除
export const delContract = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Contract/Del?id=" + id)
}
//BatchDel
export const batchDelContract = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Contract/BatchDel?ids=" + ids)
}




//职务信息模块
//获取列表
export const getPositionDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Position/GetPositions", parms)
}
//添加
export const addPosition = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Position/Add", parms)
}
//修改
export const editPosition = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Position/Edit", parms)
}
//删除
export const delPosition = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Position/Del?id=" + id)
}
//BatchDel
export const batchDelPosition = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Position/BatchDel?ids=" + ids)
}


//部门信息模块
//获取列表
export const getDepartmentDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Department/GetDepartments", parms)
}
//添加
export const addDepartment = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Department/Add", parms)
}
//修改
export const editDepartment = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Department/Edit", parms)
}
//删除
export const delDepartment = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Department/Del?id=" + id)
}
//BatchDel
export const batchDelDepartment = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Department/BatchDel?ids=" + ids)
}



//员工信息模块
//获取列表
export const getEmployeeDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Employee/GetEmployees", parms)
}
//添加
export const addEmployee = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Employee/Add", parms)
}
//修改
export const editEmployee = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Employee/Edit", parms)
}
//删除
export const delEmployee = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Employee/Del?id=" + id)
}
//BatchDel
export const batchDelEmployee = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Employee/BatchDel?ids=" + ids)
}



//菜单模块
//获取列表
export const getMenuDataNew = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Menu/GetMenus", parms)
}
//添加
export const addMenu = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Menu/Add", parms)
}
//修改
export const editMenu = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Menu/Edit", parms)
}
//删除
export const delMenu = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Menu/Del?id=" + id)
}
//BatchDel
export const batchDelMenu = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Menu/BatchDel?ids=" + ids)
}
//分配菜单
export const settingMenu = async (rid: string,mids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(`${http}/Menu/SettingMenu?rid=${rid}&mids=${mids}`)
}
//角色模块
//获取列表
export const getRoleData = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Role/GetRoles", parms)
}
//添加
export const addRole = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Role/Add", parms)
}
//修改
export const editRole = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Role/Edit", parms)
}
//删除
export const delRole = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Role/Del?id=" + id)
}
//BatchDel
export const batchDelRole = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Role/BatchDel?ids=" + ids)
}

//用户模块
//获取列表
export const getUserData = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Users/GetUsers", parms)
}
//添加
export const addUsers = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Users/Add", parms)
}
//修改
export const editUsers = async (parms: {}) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http + "/Users/Edit", parms)
}
//删除
export const delUsers = async (id: number) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Users/Del?id=" + id)
}
//BatchDel
export const batchDelUsers = async (ids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(http + "/Users/BatchDel?ids=" + ids)
}
//分配
export const settingRole = async (pid: string,rids: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(`${http}/Users/SettingRole?pid=${pid}&rids=${rids}`)
}
//根据角色获取菜单
export const getUserMenus = async () => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(`${http}/Menu/GetUserMenus`)
}
//个人中心修改用户昵称和密码
export const editNickNameOrPassword = async (nickName: string,password: string) => {
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.get(`${http}/Users/EditNickNameOrPassword?nickName=${nickName}&password=${password}`)
}