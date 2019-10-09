const axios = require('axios');

describe('ACME API test suite', () => {
    describe('Some smoke test...', () => {
        it('should succeed!', () => {
            expect(true).toBe(true);
        })
    })

    describe('When calling GET /api/values', () =>{
        const url = 'http://api:5000/api/values';

        it('should return OK', async () => {
            const response = await axios.get(url);
            expect(response.status).toBe(200);
        })

        it('should return a description', async ()=> {
            const response = await axios.get(url);
            expect(response.data).toEqual([ 'value1', 'value2', 'values3' ]);
        })
        
    })

    describe('When calling GET /api/values/1', () => {
        const baseUrl = 'http://api:5000/api';

        it('should return OK.', async () => {
            const response = await axios.get(baseUrl + '/values/1');
            expect(response.status).toBe(200);
        })

        it('should return a single value "value<id>"', async () => {
            const response = await axios.get(baseUrl + '/values/1');
            expect(response.data).toBe("value1");
        })
    })
})