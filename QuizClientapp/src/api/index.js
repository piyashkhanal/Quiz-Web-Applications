import axios from "axios";

export const Base_URL = 'http://localhost:5179/';
 export const ENDPOINTS = {
    participants:'participants',
    questions:'questions',
    getAnswers:'questions/getanswers'
}

export const createAPIEndpoint = endpoint =>{
    let url = Base_URL + 'api/' + endpoint + '/';
    return{
        fetch: () => axios.get(url),
        fetchById: id => axios.get(url + id),
        post: newRecord => axios.post(url,newRecord),
        put:(id,updatedRecord) => axios.put(url + id,updatedRecord),
        delete: id => axios.delete(url+ id),
    }
}