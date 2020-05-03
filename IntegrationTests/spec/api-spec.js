const axios = require('axios');

describe('Vice Software API test suite', () => {
    describe('When calling GET /api/documents', () =>{
        const url = 'http://api:5000/api/documents';

        it('should return OK', async () => {
            console.log(url)
            const response = await axios.get(url);
            expect(response.status).toBe(200);
        });

        it('should return a description', async ()=> {
            const response = await axios.get(url);
            expect(response.data.result.length).toBe(2);
        });
    });
});