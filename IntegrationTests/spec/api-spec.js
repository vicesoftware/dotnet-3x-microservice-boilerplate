const axios = require('axios');

const url = 'http://api:5000/api/documents';

describe('CRUD /api/documents', () => {
    it('should be able to create document, update the document and then delete it', async () => {
        const expectedDocumentName = 'expectedDocumentName';
        
        const response = await axios.post(url, { name: expectedDocumentName}, {
            headers: {
                'Accept': 'application/json',
            }
        });
        
        let thrownError;
        let getResponse;
        let updatedDocument;
        
        try {
            expect(response.status).toBe(201);

            expect(response.data.name).toBe(expectedDocumentName);

            getResponse = await axios.get(response.headers.location, {
                headers: {
                    'Accept': 'application/json',
                }
            });

            expect(getResponse.data.name).toEqual(expectedDocumentName);
            
            const expectedUpdatedDocumentName = expectedDocumentName + 'Updated';
            
            updatedDocument = Object.assign({}, getResponse.data, { name: expectedUpdatedDocumentName});
            
            const putResponse = await axios.put(response.headers.location, updatedDocument, {
                headers: {
                    'Accept': 'application/json',
                }
            }); 
            
            expect(putResponse.status).toBe(204);

        } catch (e) {
            thrownError = e;
        }

        const deleteResponse = await axios.delete(response.headers.location, {
            headers: {
                'Accept': 'application/json',
            }
        });
        
        expect(deleteResponse.status).toBe(200)
        
        expect(deleteResponse.data).toEqual(updatedDocument)

        if (thrownError) {
            // The tests failed earlier but we wanted to make sure that the DELETE call
            // still happened so our DB would be cleaned up properly so we catch the error
            // always fire our DELETE and then fail if there was an error caught by rethrowing
            // the caught error below
            throw thrownError;
        }
        
        let errorResponse = {};

        try {
            const getResponseAfterDelete = await axios.get(response.headers.location, {
                headers: {
                    'Accept': 'application/json',
                }
            });
        } catch (e) {
            errorResponse = e;
        }
        
        expect(errorResponse.response.status).toBe(404);
    });
    
    it('POST should require name', async () => {
        let errorResponse = {};

        try {
            await axios.post(url, { }, {
                headers: {
                    'Accept': 'application/json',
                }
            });
        } catch (e) {
            errorResponse = e;
        }

        expect(errorResponse.response.status).toBe(400);
        expect(errorResponse.response.data.status).toBe(400);
        expect(errorResponse.response.data.errors.Name.length).toBe(1);
        expect(errorResponse.response.data.errors.Name[0]).toBe('The Name field is required.');
    });

    it('PUT should require name', async () => {
        const expectedDocumentName = 'expectedDocumentName';

        const response = await axios.post(url, { name: expectedDocumentName}, {
            headers: {
                'Accept': 'application/json',
            }
        });

        let errorResponse;

        try {
            await axios.put(response.headers.location, {}, {
                headers: {
                    'Accept': 'application/json',
                }
            });

        } catch (e) {
            errorResponse = e;
        }

        const deleteResponse = await axios.delete(response.headers.location, {
            headers: {
                'Accept': 'application/json',
            }
        });

        expect(errorResponse.response.status).toBe(400);
        expect(errorResponse.response.data.status).toBe(400);
        expect(errorResponse.response.data.errors.Name.length).toBe(1);
        expect(errorResponse.response.data.errors.Name[0]).toBe('The Name field is required.');
    });

    it('PUT should require a valid url', async () => {
        const expectedDocumentName = 'expectedDocumentName';

        const response = await axios.post(url, { name: expectedDocumentName}, {
            headers: {
                'Accept': 'application/json',
            }
        });

        let errorResponse;

        try {
            await axios.put(`${url}/badid`, response.data, {
                headers: {
                    'Accept': 'application/json',
                }
            });

        } catch (e) {
            errorResponse = e;
        }

        const deleteResponse = await axios.delete(response.headers.location, {
            headers: {
                'Accept': 'application/json',
            }
        });

        expect(errorResponse.response.status).toBe(400);
        expect(errorResponse.response.data.status).toBe(400);
        expect(errorResponse.response.data.errors.id.length).toBe(1);
        expect(errorResponse.response.data.errors.id[0]).toBe("The value 'badid' is not valid.");
    });
});
