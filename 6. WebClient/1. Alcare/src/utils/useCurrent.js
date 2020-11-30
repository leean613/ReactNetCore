import { useEffect, useRef } from 'react';


const useCurrent = (props) => {
    const isCurrent = useRef(true);

    useEffect(() => {
        return () => {
            isCurrent.current = false;
        }
    }, []);

    return isCurrent;
}

export default useCurrent;