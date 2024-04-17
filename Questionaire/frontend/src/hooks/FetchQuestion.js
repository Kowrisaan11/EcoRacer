import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as Action from '../redux/question_reducer';
import { Questions } from "../helpers/Questions";

/** Fetch question hook to fetch API data and set value to store */
export const useFetchQuestion = () => {
    const dispatch = useDispatch();   
    const [getData, setGetData] = useState({ isLoading : false, apiData : [], serverError: null});

    useEffect(() => {
        setGetData(prev => ({...prev, isLoading : true}));

        /** Async function to fetch backend data */
        (async () => {
            try {
                if(Questions.length > 0){
                    setGetData(prev => ({...prev, isLoading : false}));
                    setGetData(prev => ({...prev, apiData : Questions}));

                    /** Dispatch an action */
                    dispatch(Action.startExamAction({ question : Questions }));

                } else{
                    throw new Error("No Question Available");
                }
            } catch (error) {
                setGetData(prev => ({...prev, isLoading : false}));
                setGetData(prev => ({...prev, serverError : error}));
            }
        })();
    }, [dispatch]);

    return [getData, setGetData];
}
