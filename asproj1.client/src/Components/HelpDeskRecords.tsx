import { useState } from 'react';
import { MDBInputGroup, MDBBtn } from 'mdb-react-ui-kit';

export default function HelpDeskRecords() {
    const [formData, setFormData] = useState({
        fullName: '',
        phoneNumber: '',
        diagnosisDetails: '',
        treatmentPlan: ''
    });

    const [input, setInput] = useState({
        emailInput: '',
        accessCodeInput: ''
    });

    const handleChange = (e: { target: { name: string; value: string; }; }) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleChangeInput = (e: { target: { name: string; value: string; }; }) => {
        const { name, value } = e.target;
        setInput(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = () => {
        const jsonData = {
            fullName: formData.fullName,
            phoneNumber: formData.phoneNumber,
            diagnosisDetails: formData.diagnosisDetails,
            treatmentPlan: formData.treatmentPlan,
            email: input.emailInput,
            accessCode: input.accessCodeInput
        };
        fetch('/api/records', {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(jsonData)
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

    const handlePostRecords = async () => {
        try {
            const response = await fetch('/api/records', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: input.emailInput,
                    accessCode: input.accessCodeInput
                })
            });

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
            <MDBInputGroup style={{ marginBottom: '10rem' }}>
                <textarea
                    className='form-control'
                    name='treatmentPlan'
                    value={formData.treatmentPlan}
                    onChange={handleChange}
                    placeholder='Treatment Plan'
                />
            </MDBInputGroup>

            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <input
                    className='form-control'
                    name='emailInput'
                    value={input.emailInput}
                    onChange={handleChangeInput}
                    placeholder='Email input'
                />
            </MDBInputGroup>
            <MDBInputGroup style={{ marginBottom: '1rem' }}>
                <input
                    className='form-control'
                    name='accessCodeInput'
                    value={input.accessCodeInput}
                    onChange={handleChangeInput}
                    placeholder='AccessCode input'
                />
            </MDBInputGroup>
            <MDBBtn onClick={handleSubmit}>Update</MDBBtn>
            <MDBBtn onClick={handlePostRecords}>Get</MDBBtn>
        </>
    );
}
