export interface UserView {
    id : number;
    status : number;
    createdAt : string;
    createdBy : string;
    updatedAt : string;
    updatedBy : string;
    username : string;
    firstName : string;
    lastName : string;
    role : number;
    userBranches : []
}