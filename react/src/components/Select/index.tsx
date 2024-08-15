import React, { useState, useContext, createContext, useEffect } from "react";

import styles from "./index.module.scss";
import classNames from "classnames";
type SelectProps<T> = {
  value?: T;
  onChange?: (newValue: T) => void;
  children?: React.ReactNode;
};

type SelectOptionProps<T> = {
  value: T;
  children?: React.ReactNode;
};

type ContextType<T> = {
  selectedValue: T;
  setSelectedValue: (value: T) => void;
  selectedOption: React.ReactNode;
  setSelectedOption: (element: React.ReactNode) => void;
};

const SelectContext = createContext<ContextType<any>>({
  selectedValue: null,
  setSelectedValue: (value) => {},
  selectedOption: null,
  setSelectedOption: (el) => {},
});

function Select<T>({ value, onChange, children }: SelectProps<T>) {
  const [showOptions, setShowOptions] = useState(false);
  const [selectedOption, setSelectedOption] = useState(null);
  return (
    <SelectContext.Provider
      value={{
        selectedValue: value,
        setSelectedValue: (value) => {
          onChange(value);
          setShowOptions(false);
        },
        selectedOption,
        setSelectedOption,
      }}
    >
      <view className={styles.container}>
        <view onClick={() => setShowOptions((prev) => !prev)}>
          {selectedOption}
        </view>
        <view
          className={styles.options}
          style={{ visibility: showOptions ? "visible" : "hidden" }}
        >
          {children}
        </view>
      </view>
    </SelectContext.Provider>
  );
}

function Option<T>({ value, children }: SelectOptionProps<T>) {
  const { selectedValue, setSelectedValue, setSelectedOption } =
    useContext<ContextType<T>>(SelectContext);

  const handleClick = () => {
    setSelectedValue(value);
  };

  useEffect(() => {
    if (selectedValue === value) {
      setSelectedOption(children);
    }
  }, [selectedValue, value, setSelectedOption, children]);

  return (
    <view
      className={classNames(styles.option, {
        [styles.selected]: selectedValue === value,
      })}
      onClick={handleClick}
    >
      {children}
    </view>
  );
}

Select.Option = Option;

export default Select;
