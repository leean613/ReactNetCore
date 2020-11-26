import React from 'react';
import Select from "react-select";
import CustomSelectInput from "./CustomSelectInput";
import "./../../../assets/css/DropdownList/dropdownList.scss";

function DropdownList(props) {
    const style = {
        control: (base, state) => ({
            ...base,
            boxShadow: state.isFocused ? 0 : 0,
            borderColor: state.isFocused ? "#9A9A9A" : base.borderColor,
            "&:hover": {
                borderColor: state.isFocused ? "#9A9A9A" : base.borderColor
            },
            transition: "color 0.3s ease-in-out, border-color 0.3s ease-in-out, background-color 0.3s ease-in-out"
        })
    };

    const handleOnChange = (option) => {
        props.handleDropdownListOnChange(option);
    }

    return (
        <Select
            components={{ Input: CustomSelectInput }}
            className={props.className ? props.className : ""}
            classNamePrefix={props.className ? props.className : ""}
            name={props.name ? props.name : ""}
            placeholder={props.placeholder ? props.placeholder : ""}
            value={props.Roles.find((x) => x.value === props.currentValue)}
            onChange={(option) => handleOnChange(option)}
            options={props.Roles}
            styles={style}
        />
    );
}

export default DropdownList;