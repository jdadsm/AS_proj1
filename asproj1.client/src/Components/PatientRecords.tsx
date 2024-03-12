import { useState } from 'react';
import { MDBInputGroup, MDBBtn } from 'mdb-react-ui-kit';

export default function PatientRecords() {
    const [formData, setFormData] = useState({
        fullName: '',
        phoneNumber: '',
        diagnosisDetails: '',
        treatmentPlan: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = () => {
        fetch('/api/records', {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        })
            .then(response => {
                if (response.ok) {
                    console.log('Form data updated successfully');
                } else {
                    console.error('Failed to update form data');
                }
            })
            .catch(error => {
                console.error('Error updating form data:', error);
            });
    };

    const handleGet = async () => {
        try {
            const response = await fetch('/api/records');

            if (!response.ok) {
                throw new Error('Failed to fetch records');
            }

            const data = await response.json();

            setFormData(data);
        } catch (error) {
            console.error('Error fetching records:', error);
        }
    };

    return (
        <>
            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <input
                    className='form-control'
                    type='text'
                    name='fullName'
                    value={formData.fullName}
                    onChange={handleChange}
                    placeholder='Full Name'
                />
            </MDBInputGroup>
            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <input
                    className='form-control'
                    type='tel'
                    name='phoneNumber'
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    placeholder='Phone Number'
                />
            </MDBInputGroup>
            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <textarea
                    className='form-control'
                    name='diagnosisDetails'
                    value={formData.diagnosisDetails}
                    onChange={handleChange}
                    placeholder='Diagnosis Details'
                />
            </MDBInputGroup>
            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <textarea
                    className='form-control'
                    name='treatmentPlan'
                    value={formData.treatmentPlan}
                    onChange={handleChange}
                    placeholder='Treatment Plan'
                />
            </MDBInputGroup>
            <MDBBtn onClick={handleSubmit}>Update</MDBBtn>
            <MDBBtn onClick={handleGet}>Get</MDBBtn>
        </>
    );
}
