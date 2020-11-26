import React, { Component } from 'react';
import { Card, CardHeader, CardBody, CardTitle, Button, } from "reactstrap";
import { uploadService } from './../../services/Upload/UploadService.js';
import { RingLoader } from "react-spinners";
class FileUploadComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedFile: '',
            status: '',
            progress: 0,
            isLoading: false
        };
    }
    selectFileHandler = (event) => {
        //1. define the array for the file type e.g. png, jpeg
        const fileTypes = ['image/png', 'image/jpeg', 'image/jpg'];

        // 2. get the file type
        let file = event.target.files;
        console.log(`File ${file}`);
        // 3. the message for error if the file type of not matched
        let errMessage = [];
        // 4. to check the file type to match with the fileTypes array iterate 
        // through the types array
        if (fileTypes.every(extension => file[0].type != extension)) {
            errMessage.push(`The file ${file.type} extension is not supported`);
        } else {
            this.setState({
                selectedFile: file[0]
            });
        }
    };

    // method contain logic to upload file
    uploadHandler = async (event) => {
        this.setState({ status: '', isLoading: true });
        // 1. the FormData object that contains the data to be posted to the WEB API
        const formData = new FormData();
        formData.append('file', this.state.selectedFile);

        // 2. post the file to the WEB API
        try {
            var { data } = await uploadService.UploadFileS3(formData);
            this.setState({ status: `${data}`, isLoading: false });
        } catch (error) {
            this.setState({ status: `${error}`, isLoading: false });
        }
    }

    render() {
        return (
            <div className="content">
                <Card>
                    <CardHeader>
                        <CardTitle tag="h5">The File Upload</CardTitle>
                    </CardHeader>
                    <div className={`login-spinner ${this.state.isLoading ? "" : "login-spinner-isVisable"}`}>
                        <RingLoader></RingLoader>
                    </div>
                    <CardBody>
                        <input type="file" onChange={this.selectFileHandler} />
                        <div className="text-danger">Support file type: (image/png, image/jpeg, image/jpg)</div>
                        <hr />
                        <div>
                            <Button color="primary" outline size="xs" className="default" onClick={this.uploadHandler}>Upload</Button>
                        </div>
                        <hr />
                        <div>{this.state.status}</div>
                    </CardBody>
                </Card>
            </div>
        );
    }
}
export default FileUploadComponent;